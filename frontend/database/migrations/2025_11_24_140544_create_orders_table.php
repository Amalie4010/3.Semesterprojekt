<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     */
    public function up(): void
    {
        Schema::create('orders', function (Blueprint $table) {
            $table->id();
            $table->foreignId('beertype_id')->constrained('beertype');
            $table->integer('quantity');
            $table->foreignId('event_id')->constrained('event'); 
            $table->timestamps();
        });
    }
    /*
        Constrained does 2 things. 
        1. creates a column in the order table.
        2. creates the foreign key constraint, meaning:
            event must refference to existing id in event table
            if an event is deleted its corresponding orders will be deleted (so that orders wont be reffering to nothing)
    */

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('orders');
    }
};
