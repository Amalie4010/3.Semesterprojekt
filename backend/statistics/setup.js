import './app.js'; //server
import './database/create_database.js'

// IMPORT DATA FROM DATABASE

import {pythonStarter, prediction} from './pyrunner.js'; //runs statistics prediction

prediction();

export let order_group = 0; //Runs statistics every 5 min
setInterval(() => {
pythonStarter(++order_group)
}, 5 * 1000);