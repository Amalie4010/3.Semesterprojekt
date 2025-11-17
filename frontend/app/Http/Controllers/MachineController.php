<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Http;

class MachineController extends Controller
{

    public function machine()
    {
        return view('index');
    }
    public function connectToMachine(Request $request)
    {
        $request->validate([
            'connectionStr' => 'required|string',
        ]);

        $connectionStr = $request->input('connectionStr');

        // Sends json 
        $response = Http::withHeaders([
            'Content-Type' => 'application/json'
        ])->send('POST', 'http://localhost:5139/api/communication/machine', [
            'body' => json_encode($connectionStr)
        ]);

        if ($response->failed()) {
            return back()->with('error', $response->body());
        }

        return back()->with('success', 'Connected successfully!');
    }

    public function Power(Request $request)
    {
        $request->validate([
            'd' => 'required|integer',
        ]);

        $state = (int)$request->input('d');

        // Send Json in the body 
        $response = Http::withHeaders([
            'Content-Type' => 'application/json',
        ])->send('POST', 'http://localhost:5139/api/communication/power', [
            'body' => json_encode($state)  //sends the state/ should be one for activating the api to connect to OPC UA machine
        ]);

        if ($response->failed()) {
            return back()->with('error', $response->body());
        }

        return back()->with('success', 'Power state changed! Response: ' . $response->body());
    }
}
