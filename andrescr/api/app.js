"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var express = require("express");
var cors = require("cors");
var app = express().use(cors());
var port = 37851;
app.get('/', function (req, res) {
    res.send('Hello, World 2!');
});
app.listen(port, function () {
    console.log("Server is running at https://localhost:" + port);
});
