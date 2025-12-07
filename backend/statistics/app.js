import routes from "./routes/routes.js";
import express from "express";

//Initialize Database
import './database/create_database.js'; //Creates the database

//Start server
const app = express ();

app.use(express.json());
app.use(express.urlencoded({extended: true}));

const PORT = 8080; //Do not change - port has been agreed upon in the group

//Use routes
app.use("/api", routes);


//Listening
app.listen(PORT, () => {
    console.log("Server Listening on PORT:", PORT);
});




>>>>>>>>> Temporary merge branch 2


>>>>>>>>> Temporary merge branch 2
