import json
import time
from flask import Flask, request

app = Flask(__name__)

emoji_dict = {"emoji": "", "count": 0}
last_time = time.time()


@app.route("/duet_emoji", methods=["GET"])
def read():
    # This isn't idempotent, which is kind of unholy, but whatever
    global last_time, emoji_dict
    temp = emoji_dict
    emoji_dict = {"emoji": "", "count": 0}
    return json.dumps(temp)


@app.route("/duet_emoji", methods=["POST"])
def write():
    global emoji_dict, last_time
    param = request.get_json(force=True, silent=True)
    emoji_dict = param if param else emoji_dict
    last_time = time.time()
    return ("", 204)


if __name__ == "__main__":
    app.run(debug=False, host="0.0.0.0")
