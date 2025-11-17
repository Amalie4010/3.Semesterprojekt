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
public function connectToMachine(Request $request){
    $request->validate([
            'connectionStr => required|string',
        ]);

        $temp = $request->input('text');

        //Encode the temp string
        $encoded = str_replace("/", "%2F", $temp);
        $encoded = str_replace(":", "%3A", $temp);

        $response = Http::post('http://localhost:5139/api/communication/machine', [
        'connectionStr' => $encoded
        ]);

        return view('machine');
}

public function Power(Request $request){
    $request->validate([
            '-d => required|number',
        ]);

        $state = $request->input('state');
        $response = Http::post('http://localhost:5139/api/communication/power', [
        '-d' => $state
        ]);

        return view('machine');
}

    public function connect(Request $request)
    {
        $request->validate([
            'connectionString => required|string',
        ]);

        $connectionString = $request->input('connectionString');

        // Send connectionstring to communication

        //debug
        $apiUrl = "http://localhost:5139/api/communication/machine";

        $response = Http::withHeaders([
            'Content-Type' => 'application/json'
        ])->post($apiUrl, $connectionString);
        //$response = Http::post('http://localhost:5139/api/communication/machine', urlencode(json_encode($connectionString)));
         

        //if ($response->failed()) {            return response()->json(['error' => 'Failed to connect to backend'], 500);}
            if ($response->failed()) {
            return response()->json([
                'success' => false,
                'error' => $response->body()
            ], 500);
        }

        return response()->json(['success' => true]);
    }

}
