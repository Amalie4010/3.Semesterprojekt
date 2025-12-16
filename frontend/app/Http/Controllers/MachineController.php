<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Http;
use Illuminate\Support\Facades\Log;

class MachineController extends Controller
{
    /**
     * Return all machines from C# API
     */
    public function listMachines()
    {
        try {
            $response = Http::get('http://localhost:5139/api/communication/machines');
            return $response->json();
        } catch (\Throwable $e) {
            Log::error("listMachines error: " . $e->getMessage());
            return response()->json(['error' => 'Failed to load machines'], 500);
        }
    }

    /**
     * The Machine Monitor View
     */
    public function machine()
    {
        return view('machine');
    }

    /**
     * Connect to OPC UA machine
     */
    public function connectToMachine(Request $request)
    {
        Log::debug("Here");
        // Validate input
        $request->validate([
            'connectionString' => 'required|string',
        ]);

        $connectionString = $request->connectionString;

        try {
            $apiUrl = 'http://localhost:5139/api/communication/machine';

            // Send connectionString as RAW JSON string to C# API
            $response = Http::withHeaders(['Content-Type' => 'application/json'])
                ->withBody(json_encode($connectionString), 'application/json')
                ->post($apiUrl);
            Log::debug("A" . $respnose);
            // If C# responded with failure
            if ($response->failed()) {
                return back()->with('error', $response->body());
                Log::debug("Shit");
            }

            // SUCCESS -> redirect back to machine monitor
            return redirect()->route('machine')
                ->with('success', 'Machine connected successfully!');

        } catch (\Throwable $e) {
            Log::error("connectToMachine error: " . $e->getMessage());
            return back()->with('error', 'Could not connect to machine: ' . $e->getMessage());
        }
    }

    /**
     * Send power signal to machine(s)
     */
    public function Power(Request $request)
    {
        $request->validate([
            'd' => 'required|integer',
        ]);

        $state = (int) $request->input('d');

        try {
            $response = Http::withHeaders([
                'Content-Type' => 'application/json',
            ])->send('POST', 'http://localhost:5139/api/communication/power', [
                'body' => json_encode($state)
            ]);

            if ($response->failed()) {
                return back()->with('error', $response->body());
            }

            return back()->with('success', 'Power state changed!');

        } catch (\Throwable $e) {
            Log::error("Power error: " . $e->getMessage());
            return back()->with('error', 'Failed to change power state.');
        }
    }

    /**
     * Emergency stop
     */
    public function Stop(Request $request)
    {
        try {
            $response = Http::withHeaders([
                'Content-Type' => 'application/json',
            ])->post('http://localhost:5139/api/communication/stop', [
                'command' => '3'
            ]);

            if ($response->failed()) {
                return back()->with('error', $response->body());
            }

            return back()->with('success', 'Machine stopped!');

        } catch (\Throwable $e) {
            Log::error("Stop error: " . $e->getMessage());
            return back()->with('error', 'Failed to stop machine.');
        }
    }
}
