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
@endsection
