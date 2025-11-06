import { spawn } from "child_process";
import { fileURLToPath } from 'url';
import { dirname } from 'path';

const data = JSON.stringify([
  {
    "beer_type": 2,
    "quantity": 5,
    "createdAt": "2025-11-04T18:02:41"
  },
  {
    "beer_type": 2,
    "quantity": 3,
    "createdAt": "2025-11-04T18:06:56"
  },
  {
    "beer_type": 1,
    "quantity": 4,
    "createdAt": "2025-11-04T18:14:10"
  },
  {
    "beer_type": 5,
    "quantity": 5,
    "createdAt": "2025-11-04T18:14:17"
  },
  {
    "beer_type": 5,
    "quantity": 5,
    "createdAt": "2025-11-04T18:16:40"
  },
  {
    "beer_type": 5,
    "quantity": 6,
    "createdAt": "2025-11-04T18:17:54"
  }
  // { beer_type: 1, quantity: 1, createdAt: "2025-11-04T14:50:14" },
  // { beer_type: 0, quantity: 2, createdAt: "2025-11-04T14:52:05" },
  // { beer_type: 1, quantity: 1, createdAt: "2025-11-04T14:54:20" },
  // { beer_type: 2, quantity: 3, createdAt: "2025-11-04T15:01:12" },
  // { beer_type: 3, quantity: 1, createdAt: "2025-11-04T15:06:30" },
  // { beer_type: 4, quantity: 2, createdAt: "2025-11-04T15:12:47" },
  // { beer_type: 5, quantity: 1, createdAt: "2025-11-04T15:18:03" }
]);

// Path to python
const __current_filepath = fileURLToPath(import.meta.url);
const python_filepath =  dirname(__current_filepath) + "/python/main.py";

// Runs python
const pythonProcess = spawn("python", [python_filepath, data]);

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