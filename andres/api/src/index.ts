import express from "express";
const app = express();
const port = 37851;

app.get( "/", ( req, res ) => {
    res.setHeader('Access-Control-Allow-Origin', 'http://localhost:8080');
    res.send( "Hello world!!" );
} );

app.listen( port, () => {
    // console.log( `server started at http://localhost:${ port }` );
} );