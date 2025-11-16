@extends('layouts.app')

@section('content')
    <h1 class="text-3xl font-bold mb-6">Machine Monitor</h1>
    <div class="container">
        <!-- Popup -->
    <div id="popup" class="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50">
        <div class="bg-white p-6 rounded-2xl shadow-lg w-96">
            <h2 class="text-xl font-semibold mb-4">Enter Connection String</h2>
            <input id="connectionString" type="text" class="border w-full rounded p-2 mb-4" placeholder="Server=...;Database=...;">
            <button onclick="connectMachine()" class="bg-blue-600 text-white px-4 py-2 rounded w-full">Connect</button>
        </div>
    </div>

    <!-- Display -->
    <div id="dataSection" class="hidden w-full max-w-2xl mt-6">
        <div class="bg-white p-6 rounded-xl shadow-lg">
            <h2 class="text-lg font-semibold mb-4">Live Machine Data</h2>
            <pre id="machineData" class="bg-gray-200 p-3 rounded text-sm overflow-auto h-96">Waiting for data...</pre>
        </div>
    </div>
    </div>
    
@endsection