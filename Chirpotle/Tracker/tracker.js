var notes = {
    "q": "c",
    "a": "c#",
    "w": "d",
    "s": "d#",
    "e": "e",
    "r": "f",
    "f": "f#",
    "t": "g",
    "g": "g#",
    "y": "a",
    "h": "a#",
    "u": "b",
};

var octave = 4;

var setOctave = function (oct) {
    octave = oct;
    $("#octaveLabel").text(octave);
}

var step = function (x, y) {
    var selected = $(".selected").first();
    var maxx = $(".track_line").length;
    var maxy = $(".track_line").first().find(".node").length;

    var newx = selected.data("x") + x;
    var newy = selected.data("y") + y;
    if (newx >= maxx) { newx = 0; }
    if (newx < 0) { newx = maxx - 1}
    if (newy >= maxy) { newy = 0; }
    if (newy < 0) { newy = maxy - 1 }

    $(".selected").removeClass("selected");
    $(".node[data-x='" + newx + "'][data-y='" + newy + "']").addClass("selected");
}

var updateTrigger = function (value) {
    var triggerNote = $(".selected .trigger_label");
    var triggerBar = $(".selected .trigger_bar_fill");
    var triggerLength = $(".selected").data("triggerLength") || 0;
    //FIXME: calculate sizes properly with border boxes
    var maxTriggerHeight = $(".node.selected").height() + 2;

    var newTriggerLength;
    var decimal = value % 1;
    if (value >= 1) {
        newTriggerLength = value
    } else {
        newTriggerLength = Math.floor(triggerLength / 1) + decimal;
    }
    $(".selected").data("triggerLength", newTriggerLength);
    triggerBar.height(maxTriggerHeight * newTriggerLength);
    triggerNote.text("'" + newTriggerLength);
}

var deleteTrigger = function () {
    $(".selected").data("triggerLength", undefined);
    $(".selected .trigger_bar_fill").height(0);
    $(".selected .trigger_label").text("");
    $(".selected .note_label").text("");
}

$(document).ready(function () {
	loadTracker($("#tracker"));

	$("body").on("keydown", function(evt) {
        //console.log(evt.keyCode);
        //console.log(evt.key);
        if (evt.keyCode >= 49 && evt.keyCode <= 56) {
            setOctave(evt.keyCode - 48);
            return;
        }

        var selected = $(".selected");
        if (selected.length == 0) { return; }
        var label = $(selected).find(".note_label").first();
        var triggerNote = $(selected).find(".trigger_label");

        if (evt.keyCode >= 96 && evt.keyCode <= 105) {
            var value = evt.keyCode - 96;
            if (!evt.altKey) { value /= 10; }
            updateTrigger(value);
            return;
        }

        switch (evt.key) {
            case ";":
                step(-1, 0);
                break;
            case "'":
                step(0, 1);
                break;
            case "\\":
                step(1, 0);
                break;
            case "[":
                step(0, -1);
                break;
            case "z":
                deleteTrigger();
                break;
            case "q":
            case "a":
            case "w":
            case "s":
            case "e":
            case "r":
            case "f":
            case "t":
            case "g":
            case "y":
            case "h":
            case "u":
                label.text(notes[evt.key] + octave);
                break;
            case "`":
                var trigger = prompt("Trigger length");
                updateTrigger(parseInt(trigger));
                break;
            case "Escape":
                console.log("esc pressed");
                $("select.instruments").remove();
                break;
        }
	});
});

var loadTracker = function (tracker) {
	for (var i = 0; i < 8; i++) {
		appendTrack(tracker, i);
	}
};

var appendTrack = function(tracker, trackId) {
	var column = $(document.createElement("div")).addClass("track_line").appendTo(tracker);
    var instrumentSelector = $(document.createElement("div")).addClass("instrument-label");
    var menu = $(document.createElement("select")).addClass("instruments");
    menu.on("click", function (evt) {
        $(menu).find("option").remove();
        var instruments = getInstruments();
        for (var i = 0; i < instruments.length; i++) {
            $("<option>" + instruments[i] + "</option>", { value: i }).appendTo(menu);
        }
    });
    instrumentSelector.append(menu);
    instrumentSelector.appendTo(column);

	for (var i=0; i < 32; i++) {
		var node = $(document.createElement("div")).addClass("node").on("click", function(evt) {
			$(".selected").removeClass("selected");
			$(evt.target).addClass("selected");
		}).attr("data-x", trackId).attr("data-y", i).appendTo(column);
        $(document.createElement("div")).addClass("trigger_bar").append(
            $(document.createElement("div")).addClass("trigger_bar_fill").height(0)
        ).appendTo(node);
		$(document.createElement("div")).addClass("note_label").appendTo(node);
		$(document.createElement("div")).addClass("trigger_label").appendTo(node);
		$(document.createElement("div")).addClass("clear").appendTo(node);
	}
	$(document.createElement("div")).addClass("clear").appendTo(column);
}

var getSongData = function() {
	var lines = [];
	var tracks = $(".track_line");
    var noteindexes = Object.keys(notes).map(function (e) {
        return notes[e];
    });
	for (var lineIndex=0; lineIndex<32; lineIndex++) {
		var line = "";
		for (var trackIndex=0; trackIndex<8; trackIndex++) {
            var node = $(tracks[trackIndex]).find(".node")[lineIndex];
            var text = $(node).find(".note_label").text();

            if (text != "") {
                var note = text.slice(0, -1);
                var octave = parseInt(text.slice(-1));
                var instrumentIndex = $($(".track_line")[trackIndex]).data("instrument-index");
                var noteidx = noteindexes.indexOf(note) + 1 + octave * 12;
                var triggerLength = $(node).data("triggerLength");
                line += instrumentIndex + ";" + triggerLength + ";" + noteidx + " ";
            } else {
                line += "- ";
            }
		}
        lines.push(line.trim());
	}
    return lines.join("|");
}

var getInstruments = function() {
    if (window.external.TrackerGetInstruments) {
        window.external.DebugLog("getting instruments");
        var instruments = window.external.TrackerGetInstruments();
        window.external.DebugLog(instruments);
    } else {
        return ["asd", "qwe"];
    }
}