const express = require( "express" );
const app = express();
const port = 37851; 

app.get( "/", (req, res ) => {
    res.send( "Hello world!" );
} );

app.listen( port, () => {
    console.log( `http://localhost:${ port }` );
} );