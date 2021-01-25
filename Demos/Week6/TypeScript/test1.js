"use strict";
var UserInputClass = /** @class */ (function () {
    function UserInputClass(remainder) {
        this.dimes = 0;
        this.nickels = 0;
        this.pennies = 0;
        this.remainder = remainder;
    }
    UserInputClass.prototype.NameLess = function () {
        while (this.remainder > 0) {
            // console.log(reminder);
            if (this.remainder >= .10) {
                //do something
                this.dimes++;
                this.remainder -= .10;
                this.remainder = this.remainder.toFixed(2);
                // console.log("enter the dimes stuff: ", dimes);
            }
            else if (this.remainder >= .05) {
                //do something   
                this.nickels++;
                this.remainder -= .05;
                this.remainder = this.remainder.toFixed(2);
                // console.log("enter the nickels stuff: ", nickels);
            }
            else // if (reminder >= .01)
             {
                this.pennies++;
                this.remainder -= .01;
                this.remainder = this.remainder.toFixed(2);
                // console.log("enter the penny stuff: ", penny);
            }
        }
    };
    UserInputClass.prototype.DomManipulation = function () {
        //Saving into the DOM stuff
        var myContainer = document.querySelector("#container");
        document.querySelectorAll("#container p")
            .forEach(function (child) { return child.remove(); });
        var dimetag = document.createElement('p');
        var nickeltag = document.createElement('p');
        var pennytag = document.createElement('p');
        dimetag.textContent = "Dimes: " + this.dimes;
        nickeltag.textContent = "Nickels: " + this.nickels;
        pennytag.textContent = "Pennys: " + this.pennies;
        myContainer === null || myContainer === void 0 ? void 0 : myContainer.appendChild(dimetag);
        myContainer === null || myContainer === void 0 ? void 0 : myContainer.appendChild(nickeltag);
        myContainer === null || myContainer === void 0 ? void 0 : myContainer.appendChild(pennytag);
        // myDimeTag?.textContent = "My value"
    };
    return UserInputClass;
}());
function OutsideFunc() {
    var userInput = document.querySelector("#txtNumber");
    var remainder = userInput.value;
    var myClass = new UserInputClass(remainder);
    myClass.NameLess();
    myClass.DomManipulation();
}
