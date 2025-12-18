//LOAD MACHINE LIST (AJAX POLLING)
document.addEventListener("DOMContentLoaded", () => {
    loadMachines();
    setInterval(loadMachines, 2000); // check every 2 seconds
});


function loadMachines() {
    fetch("/machines/list")
        .then(res => res.json())
        .then(machines => {
            const container = document.getElementById("machinesContainer");
            const template = document.getElementById("machine-template");

            machines.forEach(connectionString => {
                const id = btoa(connectionString);

                // Skip if box already exists
                if (document.getElementById(id)) return;

                // Clone template
                const box = template.content.cloneNode(true).children[0];
                box.id = id;

                // Insert title and data box id
                box.querySelector(".machine-title").textContent = connectionString;
                box.querySelector(".machine-data").id = `data-${id}`;
                box.querySelector(".machine-data").textContent = "Waiting for data...";

                // Append to container
                container.appendChild(box);

                // Start SSE for live updates
                startSSE(connectionString, id);
            });

        })
        .catch(err => console.error("Failed to load machines:", err));
}


// START SSE STREAM FOR MACHINE
function startSSE(connectionString, boxId) {
    const sseUrl = `http://localhost:5139/api/communication/SSEMachine?connectionString=${encodeURIComponent(connectionString)}`;
    const eventSource = new EventSource(sseUrl);

    const powerMap = { 0: "Off", 1: "On" };
    const stateMap = {
        0: "Deactivated", 1: "Clearing", 2: "Stopped", 3: "Starting",
        4: "Idle", 5: "Suspended", 6: "Execute", 7: "Stopping",
        8: "Aborting", 9: "Aborted", 10: "Holding", 11: "Held",
        15: "Resetting", 16: "Completing", 17: "Complete",
        18: "Deactivating", 19: "Activating"
    };

    eventSource.onmessage = event => {
        try {
            const data = JSON.parse(event.data);

            const formatted = {
                PowerState: powerMap[data.PowerState] ?? "Unknown",
                StateCurrent: stateMap[data.StateCurrent] ?? "Unknown",
                CurrentMachSpeed: data.CurrentMachSpeed,
                ProducedAmount: data.ProducedAmount,
                Barley: data.Barley,
                Hops: data.Hops,
                Malt: data.Malt,
                Wheat: data.Wheat,
                Yeast: data.Yeast
            };

            document.getElementById(`data-${boxId}`).textContent =
                JSON.stringify(formatted, null, 2);

        } catch (err) {
            console.error("Error parsing SSE data:", err);
        }
    };

    eventSource.onerror = () => {
        document.getElementById(`data-${boxId}`).textContent =
            "❌ SSE connection lost";
    };
}

function loadCommands() {
    Promise.all([
        fetch("/api/communication/command/current").then(res => res.json()),
        fetch("/api/communication/command/queue").then(res => res.json())
    ])
    .then(([current, queue]) => {

        // Render current commands as JSON
        document.getElementById("currentCommands").textContent =
            JSON.stringify(current, null, 2);

        // Render queue as interactive list
        renderQueue(queue);

    })
    .catch(err => {
        console.error("Failed to load commands:", err);
        document.getElementById("queueList").innerHTML = "<p>Error loading queue</p>";
        document.getElementById("currentCommands").textContent = "Error loading current commands";
    });
}

function renderQueue(queue) {
    const container = document.getElementById("queueList");

    if (!queue || queue.length === 0) {
        container.innerHTML = "<p>No commands in queue</p>";
        return;
    }

    container.innerHTML = ""; // Clear old items

    queue.forEach(cmd => {
        // Create command item wrapper
        const row = document.createElement("div");
        row.classList.add("queue-item");

        row.innerHTML = `
            <pre class="cmd-text">${JSON.stringify(cmd, null, 2)}</pre>
            <button class="delete-btn" data-id="${cmd.id}">Delete</button>
        `;

        container.appendChild(row);
    });

    // Attach event listeners for delete buttons
    document.querySelectorAll(".delete-btn").forEach(btn => {
        btn.addEventListener("click", () => {
            const id = btn.dataset.id;
            deleteCommand(id);
        });
    });
}

function deleteCommand(id) {
    fetch(`/api/communication/command/${id}`, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json"
        }
    })
    .then(res => {
        if (!res.ok) {
            throw new Error("Failed to delete the command");
        }
        return res.text();
    })
    .then(() => {
        loadCommands(); // Refresh queue immediately
    })
    .catch(err => {
        console.error("Delete error:", err);
        alert("Unable to delete command");
    });
}


