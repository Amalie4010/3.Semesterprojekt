import routes from "./routes/routes.js";
import express from "express";
const app = express ();

app.use(express.json());
app.use(express.urlencoded({extended: true}));

const PORT = 3000;

//Use routes
app.use("/api/statistics", routes);


//Listening
app.listen(PORT, () => {
    console.log("Server Listening on PORT:", PORT);
});



