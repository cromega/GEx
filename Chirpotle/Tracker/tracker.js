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

var Tracker = {
    config: {
        trackLength: 32,
        trackNum: 8,
    },
    state: {
        octave: 4,
        selected: [0,0],
    },
    project: {
        instruments: [],
    },
}

var setOctave = function (change) {
    newOctave = Tracker.state.octave + change;
    if (newOctave < 1 || newOctave > 8) { return; }
    Tracker.state.octave = newOctave;
}

var step = function (x, y) {
    var selected = $(".selected").first();
    var maxx = Tracker.config.trackNum;
    var maxy = Tracker.config.trackLength;

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
    
    rivets.bind($("#configuration"), {state: Tracker.state});

    $(".node").on("click", function(evt) {
        $(".selected").removeClass("selected");
        $(evt.target).addClass("selected");
    })
    $("select.instruments").on("change", function (evt) {
        var option = $(evt.target).find("option:selected");
		var instrument = $(option).text();
        bindInstrumentToTrack(instrument, $(option).closest(".track-line"));
    })

	$("body").on("keydown", function(evt) {
        handleKeyPress(evt);
	});
});

var loadTracker = function (tracker) {
	for (var i = 0; i < Tracker.config.trackNum; i++) {
		appendTrack(tracker, i);
	}
};

var appendTrack = function(tracker, trackId) {
    var template = $(document.getElementById("template:track")).html();
    var track = $(template).appendTo(tracker);

	for (var i=0; i < Tracker.config.trackLength; i++) {
       var template = $(document.getElementById("template:node")).html();
       var node = $(template);
       $(node).attr("data-x", trackId).attr("data-y", i).appendTo(track);
	}
	$(document.createElement("div")).addClass("clear").appendTo(track);
}

var bindInstrumentToTrack = function (name, track) {
    $(track).attr("data-instrument-index", Tracker.project.instruments.indexOf(name));
    $(track).attr("data-instrument-name", name);
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

var handleKeyPress = function(evt) {
    // console.log(evt.keyCode);
    // console.log(evt.key);

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
            label.text(notes[evt.key] + Tracker.state.octave);
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
}

//functions for external call
var addInstrument = function (instrument) {
	Tracker.project.instruments.push(instrument);
    var menus = $("select.instruments");
    $(menus).each(function (idx, menu) {
        $("<option>" + instrument + "</option>").attr("value", $(menus).first().find("option").length - 1).appendTo(menu);
    })

    if ($(menus).first().find("option").length == 1) {
        $(".track-line").each(function (idx, track) {
            bindInstrumentToTrack(instrument, track);
        });
    }
};

var removeInstrument = function(instrument) {
	Tracker.project.instruments.splice(Project.instruments.indexOf(instrument), 1);
	$("option").filter(function() { return $(this).html() == instrument; }).remove();
	debugger;
	$(".track-line").filter(function() { return $(this).data("instrument-name") == instrument; }).find(".node").text("");
	if (Project.instruments.length > 0) {
		$(".track-line").each(function(idx, track) {
			bindInstrumentToTrack(Project.instruments[0], track);
		});
	}
};