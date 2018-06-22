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

var setOctave = function (change) {
    if (octave == 1 && change == -1) { return; }
    if (octave == 8 && change == 1) { return; }
    octave += change;
    $("#octaveLabel").text(octave);
}

var step = function (x, y) {
    var selected = $(".selected").first();
    var maxx = $(".track-line").length;
    var maxy = $(".track-line").first().find(".node").length;

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
    var triggerNote = $(".selected .trigger-label");
    var triggerBar = $(".selected .trigger-bar-fill");
    var triggerLength = $(".selected").data("triggerLength") || 0;
    //FIXME: calculate sizes properly with border boxes
    var maxTriggerHeight = $(".node.selected").height() + 2;

    var newTriggerLength;
    var decimal = value % 1;
    if (value >= 1) {
        newTriggerLength = value
    } else {
        //newTriggerLength = Math.floor(triggerLength / 1) + decimal;
        newTriggerLength = decimal;
    }
    $(".selected").data("triggerLength", newTriggerLength);
    triggerBar.height(maxTriggerHeight * newTriggerLength);
    triggerNote.text("'" + newTriggerLength);
}

var deleteTrigger = function () {
    $(".selected").data("triggerLength", undefined);
    $(".selected .trigger-bar-fill").height(0);
    $(".selected .trigger-label").text("");
    $(".selected .note-label").text("");
}

$(document).ready(function () {
	loadTracker($("#tracker"));

	$("body").on("keydown", function(evt) {
        console.log(evt.keyCode);
        console.log(evt.key);

        switch (evt.key) {
            case "/":
                setOctave(1);
                break;
            case ".":
                setOctave(-1);
                break;
        }

        var selected = $(".selected");
        if (selected.length == 0) { return; }
        var label = $(selected).find(".note-label").first();
        var triggerNote = $(selected).find(".trigger-label");

        if (evt.keyCode >= 48 && evt.keyCode <= 57) {
            var value = evt.keyCode - 48;
            if (!evt.shiftKey) { value /= 10; }
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

var bindInstrumentToTrack = function (index, track) {
    console.log(index);
    $(track).data("instrument-index", index);
}

var appendTrack = function(tracker, trackId) {
	var column = $(document.createElement("div")).addClass("track-line").appendTo(tracker);
    var instrumentSelector = $(document.createElement("div")).addClass("instrument-label");
    var menu = $(document.createElement("select")).addClass("instruments");
    menu.on("change", function (evt) {
        var option = $(menu).find("option:selected");
        console.log(option);
        bindInstrumentToTrack($(option).attr("value"), $(option).closest(".track-line"));
    })
    instrumentSelector.append(menu);
    instrumentSelector.appendTo(column);

	for (var i=0; i < 32; i++) {
		var node = $(document.createElement("div")).addClass("node").on("click", function(evt) {
			$(".selected").removeClass("selected");
			$(evt.target).addClass("selected");
		}).attr("data-x", trackId).attr("data-y", i).appendTo(column);
        $(document.createElement("div")).addClass("trigger-bar").append(
            $(document.createElement("div")).addClass("trigger-bar-fill").height(0)
        ).appendTo(node);
		$(document.createElement("div")).addClass("note-label").appendTo(node);
		$(document.createElement("div")).addClass("trigger-label").appendTo(node);
		$(document.createElement("div")).addClass("clear").appendTo(node);
	}
	$(document.createElement("div")).addClass("clear").appendTo(column);
}

var getSongData = function() {
	var lines = [];
	var tracks = $(".track-line");
    var noteindexes = Object.keys(notes).map(function (e) {
        return notes[e];
    });
	for (var lineIndex=0; lineIndex<32; lineIndex++) {
		var line = "";
		for (var trackIndex=0; trackIndex<8; trackIndex++) {
            var node = $(tracks[trackIndex]).find(".node")[lineIndex];
            var text = $(node).find(".note-label").text();

            if (text != "") {
                var note = text.slice(0, -1);
                var octave = parseInt(text.slice(-1));
                var instrumentIndex = $($(".track-line")[trackIndex]).data("instrument-index");
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

var addInstrument = function (instrument) {
    var menus = $("select.instruments");
    $(menus).each(function (idx, menu) {
        $("<option>" + instrument + "</option>").attr("value", $(menus).first().find("option").length - 1).appendTo(menu);
    })

    if ($(menus).first().find("option").length == 1) {
        $(".track-line").each(function (idx, track) {
            bindInstrumentToTrack(0, track);
        });
    }
};