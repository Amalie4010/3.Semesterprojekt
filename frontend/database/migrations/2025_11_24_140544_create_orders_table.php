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
            $table->unsignedTinyInteger('type_id');
            // first "beertype" the column in current table
            // second the column in another table
            // last is the other table name were refferencing from
            $table->foreign('type_id')->references('type_id')->on('beertype');
            $table->integer('quantity');
            // automatically creates a "event_id" column inside this table, and refferences id in the "event" table, it knows that automatically, cuz that is how laravel have made it work so it calls the PK when using this refferencing method.
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
