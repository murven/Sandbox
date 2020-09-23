"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = __importDefault(require("express"));
const app = express_1.default();
const port = 37851;
app.get("/", (req, res) => {
    res.setHeader('Access-Control-Allow-Origin', 'http://localhost:8080');
    res.send("Hello world!!");
});
app.listen(port, () => {
    // console.log( `server started at http://localhost:${ port }` );
});
//# sourceMappingURL=index.js.map