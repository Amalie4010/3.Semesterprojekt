import {
  sendInstructions,
  receiveDatabaseInformation,
} from "../controllers/statisticsController.js";
import express from "express";

const router = express.Router();

export default router;

//Send instructions to communications
router.post("/", sendInstructions);

//Get information of earlier events from database
router.get("/", receiveDatabaseInformation);