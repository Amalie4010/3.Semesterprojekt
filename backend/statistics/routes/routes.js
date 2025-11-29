import {
  sendInstructions,
  receiveDatabaseInformation,
  receiveOrder,
} from "../controllers/statisticsController.js";
import express from "express";

const router = express.Router();

export default router;

//Send instructions to communications
router.post("/communication/command", sendInstructions);

//Get information of earlier events from database
router.get("/db", receiveDatabaseInformation);


router.post("/statistics/order", receiveOrder);