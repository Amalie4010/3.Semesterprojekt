import './app.js'; //server
import { spawn } from "child_process";
import { fileURLToPath } from 'url';
import { dirname } from 'path';

// IMPORT DATA FROM DATABASE

import {pythonStarter, prediction} from './pyrunner.js'; //runs statistics prediction

prediction();

export let order_group = 0; //Runs statistics every 5 min
setInterval(() => {
pythonStarter(++order_group)
}, 5 * 1000);