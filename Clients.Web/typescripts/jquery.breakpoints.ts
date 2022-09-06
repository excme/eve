export class BootStrapBreakpoints {
    constructor() {
        let _ = this;
        this.currentBp = this.getBreakpoint();

        if ($.isFunction($(window).on)) {
            $(window).on("resize." + this.n, function (e) {
                if (_.resizeThresholdTimerId) {
                    clearTimeout(_.resizeThresholdTimerId);
                }

                _.resizeThresholdTimerId = window.setTimeout(function (e) {
                    _._resizeCallback();
                    _._compareTrigger();
                }, _.defaults.buffer);
            });
        }

        if (this.defaults.triggerOnInit) {
            window.setTimeout(function () {
                $(window).trigger("breakpoint-change", {
                    type: "breakpoint-change",
                    from: _.currentBp,
                    to: _.currentBp,
                    initialInit: true
                });
            }, _.defaults.buffer);
        }

        window.setTimeout(function () {
            _._compareTrigger();
        }, 100);
    }

    defaults = {
        breakpoints: [
            { name: "xs", width: 0, inside: null, less: null, greater: null, greaterEqual: null },
            { name: "sm", width: 576, inside: null, less: null, greater: null, greaterEqual: null },
            { name: "md", width: 768, inside: null, less: null, greater: null, greaterEqual: null },
            { name: "lg", width: 992, inside: null, less: null, greater: null, greaterEqual: null },
            { name: "xl", width: 1200, inside: null, less: null, greater: null, greaterEqual: null }
        ],
        buffer: 300,
        triggerOnInit: false,
        outerWidth: false
    };

    n = "breakpoints";
    currentBp = null;
    resizeThresholdTimerId:number = null;

    getBreakpoint():string {
        var winW = this._windowWidth();
        var bps = this.defaults.breakpoints;
        var bpName;

        bps.forEach(function (bp) {
            if (winW >= bp.width) {
                bpName = bp.name;
            }
        });

        // Fallback to largest breakpoint.
        if (!bpName) {
            bpName = bps[bps.length - 1].name;
        }

        return bpName;
    };


    getBreakpointWidth(bpName:string):number {
        var bps = this.defaults.breakpoints;
        var bpWidth;

        bps.forEach(function (bp) {
            if (bpName == bp.name) {
                bpWidth = bp.width;
            }
        });

        return bpWidth;
    };

    compareCheck(check, checkBpName, callback) {
        var winW = this._windowWidth();
        var bps = this.defaults.breakpoints;
        var bpWidth = this.getBreakpointWidth(checkBpName);
        var isBp = false;

        switch (check) {
            case "lessThan":
                isBp = winW < bpWidth;
                break;
            case "lessEqualTo":
                isBp = winW <= bpWidth;
                break;
            case "greaterThan":
                isBp = winW > bpWidth;
                break;
            case "greaterEqualTo":
                isBp = winW > bpWidth;
                break;
            case "inside":
                var bpIndex = bps.findIndex(function (bp) {
                    return bp.name === checkBpName;
                });

                if (bpIndex === bps.length - 1) {
                    isBp = winW > bpWidth;
                } else {
                    var nextBpWidth = this.getBreakpointWidth(bps[bpIndex + 1].name);
                    isBp = winW >= bpWidth && winW < nextBpWidth;
                }
                break;
        }

        if (isBp) {
            callback();
        }
    };

    destroy() {
        $(window).unbind(this.n);
    };

    _compareTrigger() {
        var winW = this._windowWidth();
        var bps = this.defaults.breakpoints;
        var currentBp = this.currentBp;

        bps.forEach(function (bp) {
            if (currentBp === bp.name) {
                if (!bp.inside) {
                    $(window).trigger('inside-' + bp.name);
                    bp.inside = true;
                }
            } else {
                bp.inside = false;
            }

            if (winW < bp.width) {
                if (!bp.less) {
                    $(window).trigger('lessThan-' + bp.name);
                    bp.less = true;
                    bp.greater = false;
                    bp.greaterEqual = false;
                }
            }

            if (winW >= bp.width) {
                if (!bp.greaterEqual) {
                    $(window).trigger('greaterEqualTo-' + bp.name);
                    bp.greaterEqual = true;
                    bp.less = false;
                }

                if (winW > bp.width) {
                    if (!bp.greater) {
                        $(window).trigger('greaterThan-' + bp.name);
                        bp.greater = true;
                        bp.less = false;
                    }
                }
            }
        });
    };

    _windowWidth():number {
        var win = $(window);

        if (this.defaults.outerWidth) {
            return win.outerWidth();
        }

        return win.width();
    }

    _resizeCallback() {
        let _ = this;
        var newBp = _.getBreakpoint();

        if (newBp !== _.currentBp) {
            $(window).trigger("breakpoint-change", {
                type: "breakpoint-change", 
                from: _.currentBp,
                to: newBp
            });

            _.currentBp = newBp;
        }
    };
}