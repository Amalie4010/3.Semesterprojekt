@extends('layouts.app')

@section('content')
<header class="topnav">
    <h2>Operator Page</h2>
</header>

<main class="content">

    <!-- Left Section -->
    <section class="section status-section">
        <h3>Operational Status</h3>
        <div class="data-box" id="statusBox">
            <p id="statusText">Loading...</p>
        </div>
    </section>

    <!-- Right Section -->
    <section class="section producing-section">
        <h3>Producing</h3>
        <div class="data-box" id="producingBox">
            <p id="producingData">Waiting for data...</p>
        </div>
        <p class="total"><strong>Total Produced:</strong> <span id="totalProduced">0</span></p>
    </section>

</main>

<!-- Control Buttons -->
<div class="buttons">
    <button id="startBtn" class="btn">Start</button>
    <button id="stopBtn" class="btn">Stop</button>
</div>

<footer class="footer">
    <div class="footer-container">
        <div class="footer-column">
            <p>&copy; {{ date('Y') }} Operator Dashboard</p>
        </div>
    </div>
</footer>

<script>
// Live data polling every 2 seconds
setInterval(fetchLiveData, 2000);

async function fetchLiveData() {
    try {
        const res = await fetch('/api/operator/status');
        if (!res.ok) throw new Error('Network error');
        const data = await res.json();

        document.getElementById('statusText').innerText = data.status;
        document.getElementById('producingData').innerText = data.current_item || '—';
        document.getElementById('totalProduced').innerText = data.total_produced;
    } catch (err) {
        console.error('Fetch error:', err);
    }
}

document.getElementById('startBtn').addEventListener('click', async () => {
    await fetch('/api/operator/start', { method: 'POST' });
});
document.getElementById('stopBtn').addEventListener('click', async () => {
    await fetch('/api/operator/stop', { method: 'POST' });
});
</script>
@endsection
