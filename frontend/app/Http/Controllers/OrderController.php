<?php

namespace App\Http\Controllers;

use App\Models\Order;
use Illuminate\Http\Request;

class OrderController extends Controller
{
    // Show all orders function
    public function getOrders()
    {
        return response()->json(Order::all());
    }

    // Create a new order function
    public function createOrder(Request $request)
    {
        //validating request data so every input from the form is correct!
        $data = $request->validate([
            'beer_type' => 'required|numeric',
            'quantity' => 'required|numeric'
        ]);

        //Here we make a new varaible and use a static method from Product model class and use the create() method to store $data from before into the database
        Order::create($data);

        /* Send response back with message in the header and data as data and with status code */
        return response()->json([
            'message' => 'Order saved successfully',
            'data' => $data
        ], 201);
    }
}
