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
        Schema::create('beertype', function (Blueprint $table) {
            $table->id();
            $table->unsignedTinyInteger('type_id')->unique(); // the reason for creating another type of id, is because the machine reads one of the beer types as a 0, and the primary key cannot be saved as a 0.
            $table->string('name');
            $table->timestamps();
        });
        
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('beertype');
    }
};
