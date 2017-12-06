import json
from flask import Flask, request

app = Flask(__name__)

emoji_dict = {}


@app.route("/duet_emoji", methods=["GET"])
def read():
    return json.dumps(emoji_dict)


@app.route("/duet_emoji", methods=["POST"])
def write():
    global emoji_dict
    param = request.get_json(force=True, silent=True)
    emoji_dict = param if param else emoji_dict
    return ("", 204)


if __name__ == "__main__":
    app.run(debug=False, host="0.0.0.0")
