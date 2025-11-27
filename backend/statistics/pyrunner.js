import { spawn } from "child_process";
import { fileURLToPath } from 'url';
import { dirname } from 'path';
import fs from 'fs';

function pystarter(order_group){

  const __current_filepath = fileURLToPath(import.meta.url);
  const json_filepath =  dirname(__current_filepath) + "/orders.json";

  let orders = [];
  const json = fs.readFileSync(json_filepath, "utf8").trim();

  if (json.length > 0) {
    orders = JSON.parse(json);
      
    let data = JSON.stringify(orders);

    // Path to python
    const python_filepath =  dirname(__current_filepath) + "/python/main.py";

    // Runs python
    const pythonProcess = spawn("python", [python_filepath, data, order_group]);

    // Show output
    pythonProcess.stdout.on("data", (data) => {
      console.log(`Python output: ${data}`);
    });

    // Show error
    pythonProcess.stderr.on("data", (data) => {
      console.error(`Python error: ${data}`);
    });

    // Show process
    pythonProcess.on("close", (code) => {
      console.log(`Python process exited with code ${code}`);
    });
  }
  fs.writeFileSync(json_filepath, '');
}



function predictionSpawner(){
// Path to python
const __current_filepath = fileURLToPath(import.meta.url);
const python_filepath =  dirname(__current_filepath) + "/python/pythonOld/calculate_previous_events.py";

// Runs python
const pythonProcess = spawn("python", [python_filepath]);

// Show output
pythonProcess.stdout.on("data", (data) => {
  console.log(`Python output: ${data}`);
});

// Show error
pythonProcess.stderr.on("data", (data) => {
  console.error(`Python error: ${data}`);
});

// Show process
pythonProcess.on("close", (code) => {
  console.log(`Python process exited with code ${code}`);
});
}

export {pystarter as 'pythonStarter', predictionSpawner as 'prediction'};