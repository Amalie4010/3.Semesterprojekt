async function connectMachine() {
    const connectionString = document.getElementById('connectionString').value.trim();
    if (!connectionString) {
        alert("Please enter a connection string");
        return;
    }

    try {
        const csrfToken = document.querySelector('meta[name="csrf-token"]').content;

        // Call Laravel endpoint which forwards request to .NET backend
        const response = await fetch('/connect', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'X-CSRF-TOKEN': document.querySelector('meta[name="csrf-token"]').content
            },
            body: JSON.stringify({ connectionString }),
        });
        //crsfToken

        const data = await response.json();
        console.log("Laravel → ASP.NET response:", result);
        //if (!data.success) throw new Error("Failed to connect");
        if (!result.success) {
            alert("ASP.NET error: " + result.error);
            return;
        }

        // Hide popup
        document.getElementById('popup').classList.add('hidden');
        document.getElementById('dataSection').classList.remove('hidden');

        // Start SSE stream directly from backend
        const eventSource = new EventSource(
            `http://localhost:5139/api/communication/SSEMachine?connectionString=${encodeURIComponent(connectionString)}`
        );

        eventSource.onmessage = function (event) {
            const jsonData = JSON.parse(event.data);
            document.getElementById('machineData').textContent =
                JSON.stringify(jsonData, null, 2);
        };

        eventSource.onerror = function () {
            console.error("SSE connection lost");
            eventSource.close();
        };

    } catch (error) {
        alert("Error: " + error.message);
        console.error(error);
    }
}
