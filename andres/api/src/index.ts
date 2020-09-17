import express from "express";
const app = express();
const port = 37851;
app.all('*', function(req, res, next) {
    res.header("Access-Control-Allow-Origin", "http://localhost:8080/");
    res.header("Access-Control-Allow-Headers", "X-Requested-With");
    next();
 });
app.get( "/", ( req, res ) => {
    res.send( "Hello world!" );
} );
app.listen( port, () => {
    // console.log( `server started at http://localhost:${ port }` );
} );