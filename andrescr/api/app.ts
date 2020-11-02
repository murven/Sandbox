import express = require('express');
import cors = require('cors');
import {Request, Response} from 'express';

const app: express.Application = express().use(cors());
const port = 37851;


app.get('/', (req: Request, res: Response) => {
    res.send('Hello, World!');
});

app.listen(port, () => {
    console.log(`Server is running at http://localhost:${port}`);
});