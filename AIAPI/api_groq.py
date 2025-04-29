from flask import Flask, request, jsonify
import os
from groq import Groq

app = Flask(__name__)

client = Groq(
    api_key=os.environ.get("LAPTIMEBASE_API_KEY"),
)

if __name__ == '__main__':
    app.run(port=5000)