import csv
import logging
import time
from collections import Counter
from datetime import datetime

import requests

from read import (getUser, getMessage, getChannelname, getBannedUser,
                  getBannedChannelname)
from read import getOwner, getTurbo, getSub, getMod
from twitch_socket import openSocket, sendMessage
from settings import AWS_ENDPOINT

# Actually joins the rooms
s = openSocket()

# joinRoom(s)
readbuffer = ""

message_id = 0

# Global constants
EMOJIS = set(line.strip() for line in open('lol3.txt'))
SAMPLE_INTERVAL = 10

# Global variables for dictionary
emoji_counter = Counter()
end_time = time.time() + SAMPLE_INTERVAL


# Message function
def send(message):
    sendMessage(s, message, 0)


def game_logic(message):
    global end_time, emoji_counter
    blocks = message.split()

    # Check word against list of emojis
    word = blocks[0]
    if word in EMOJIS:
        emoji_counter[word] += 1

    if time.time() > end_time:
        # Takes top spammed emojis and converts into dictionary
        pair_in_list = emoji_counter.most_common(1)
        if pair_in_list:
            (emoji_name, emoji_count) = pair_in_list[0]
        else:
            (emoji_name, emoji_count) = ("", 0)
        requests.post("http://" + AWS_ENDPOINT + ":5000/duet_emoji",
                      json={"emoji": emoji_name, "count": emoji_count})
        # Reset state
        emoji_counter = Counter()
        end_time = time.time() + SAMPLE_INTERVAL


while True:
    # Pulls a chunk off the buffer, puts it in "temp"
    readbuffer = readbuffer + s.recv(1024).decode('utf-8')
    temp = readbuffer.split("\n")
    readbuffer = temp.pop()

    # Iterates through the chunk
    for line in temp:
        print(line)
        message_id += 1

        # Parses lines and writes them to the file
        if "PRIVMSG" in line:
            try:
                # Gets user, message, and channel from a line
                user = getUser(line)
                message = getMessage(line)
                channelname = getChannelname(line)
                owner = getOwner(line)
                mod = getMod(line)
                sub = getSub(line)
                turbo = getTurbo(line)

                game_logic(message)

                if owner == 1:
                    mod = 1

                # Writes Message ID, channel, user, date/time,
                # and cleaned message to file
                with open('outputlog.csv', 'a') as fp:
                    ab = csv.writer(fp, delimiter=',')
                    data = [
                        message_id, channelname, user,
                        datetime.now(),
                        message.strip(), owner, mod, sub, turbo
                    ]
                    ab.writerow(data)

            # Survives if there's a message problem
            except Exception as e:
                logging.exception("ERROR")

        # Responds to PINGs from twitch so it doesn't get disconnected
        elif "PING" in line:
            try:
                separate = line.split(":", 2)
                s.send(("PONG %s\r\n" % separate[1]).encode('utf-8'))
                print(("PONG %s\r\n" % separate[1]))
                print("I PONGED BACK")

            # Survives if there's a ping problem
            except:
                print("PING problem PING problem PING problem")

        # Parses ban messages and writes them to the file
        elif "CLEARCHAT" in line:
            try:

                # Gets banned user's name and channel name from a line
                user = getBannedUser(line)
                channelname = getBannedChannelname(line)

                # Writes Message ID, channel, user, date/time, and an
                # indicator that it was a ban message.
                # I use "oghma.ban" because the bot's name is oghma, and I
                # figure it's not a phrase that's likely to show up in a
                # message so it's easy to search for.
                with open('outputlog.csv', 'a') as fp:
                    ab = csv.writer(fp, delimiter=',')
                    data = [
                        message_id, channelname, user,
                        datetime.now(), "oghma.ban"
                    ]
                    ab.writerow(data)

            # Survives if there's a ban message problem
            except Exception as e:
                print("BAN PROBLEM")
                print(line)
                print(e)
