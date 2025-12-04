@extends('layouts.app')

@section('content')
<div class="monitor-container">
    <h1 class="monitor-title">Machine Monitor</h1>

    <!-- Connection Form -->
    <div class="connection-block">
        <form action="{{ route('connect') }}" method="post" class="connection-form">
            @csrf
            <input type="text" id="connectionString" name="connectionString" placeholder="Connection String">
            <button type="submit">Submit</button>
        </form>

        <form action="{{ route('Power') }}" method="post">
            @csrf
            <input type="hidden" name="d" value="1">
            <button type="submit">Start</button>
        </form>

        <form action="{{ route('Power') }}" method="post">
            @csrf
            <input type="hidden" name="d" value="0">
            <button type="submit">Stop</button>
        </form>
    </div>

    <!-- Machine container (empty - JS inserts boxes) -->
    <div class="machines-container" id="machinesContainer">

        <!-- Template used for cloning (will not be rendered by browser) -->
        <template id="machine-template">
            <div class="machine-box">
                <h2 class="machine-title"></h2>
                <pre class="machine-data"></pre>
            </div>
        </template>
    </div>

    <div class="command-container">
        <h2 class="queue-title">Production Queue</h2>

        <div class="command-section">
            <h3>Current Commands</h3>
            <pre class="command-data" id="currentCommands">Loading...</pre>
        </div>

        <div class="command-section">
            <h3>Queue</h3>
            <div id="queueList">Loading...</div>
        </div>
    </div>

</div>
@endsection

