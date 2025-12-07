import { spawn } from "child_process";
import { fileURLToPath } from 'url';
import { dirname } from 'path';
import fs from 'fs';

// Get path ----------------------------------
function getPythonPath() {
  const __current_filepath = fileURLToPath(import.meta.url);
  const base = dirname(__current_filepath);

  const win = base + "/venv/Scripts/python.exe";

  if (fs.existsSync(win)) return win;

  console.error("Could not find venv Python at:", win);
  process.exit(1);
}

const python_path = getPythonPath();
// -------------------------------------------

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
    const pythonProcess = spawn(python_path, [python_filepath, data, order_group]);

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
const pythonProcess = spawn(python_path, [python_filepath]);

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