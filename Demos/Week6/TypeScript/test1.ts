class UserInputClass {


    remainder:any;
    dimes:number = 0;
    nickels: number = 0;
    pennies:number = 0;
   
    constructor(remainder:any){
        this.remainder = remainder;
    }

    NameLess()
    {
        while (this.remainder > 0) {
            // console.log(reminder);
            if ( this.remainder >= .10 )
            {
                //do something
                this.dimes++;
                this.remainder -= .10;
                this.remainder = this.remainder.toFixed(2)
                // console.log("enter the dimes stuff: ", dimes);
                
            }
            else if ( this.remainder >= .05)
            {
                //do something   
                this.nickels++;
                this.remainder -= .05;
                this.remainder = this.remainder.toFixed(2)
                // console.log("enter the nickels stuff: ", nickels);
            }
            else // if (reminder >= .01)
            {
                this.pennies++;
                this.remainder -= .01;
                this.remainder = this.remainder.toFixed(2)
                // console.log("enter the penny stuff: ", penny);
            }
    
        }
        
    }

    DomManipulation(){
        //Saving into the DOM stuff
        let myContainer = document.querySelector("#container");
    
        document.querySelectorAll("#container p")
            .forEach( (child)=> child.remove() );
    
        let dimetag = document.createElement('p');
        let nickeltag = document.createElement('p');
        let pennytag = document.createElement('p');
    
        dimetag.textContent = "Dimes: " + this.dimes;
        nickeltag.textContent = "Nickels: " + this.nickels;
        pennytag.textContent = "Pennys: " + this.pennies;
    
        myContainer?.appendChild(dimetag);
        myContainer?.appendChild(nickeltag);
        myContainer?.appendChild(pennytag);
        // myDimeTag?.textContent = "My value"
    }


}


function OutsideFunc(){
    
    let userInput: any = document.querySelector("#txtNumber");
    let remainder :any = userInput.value;
    
    let myClass = new UserInputClass(remainder);
    myClass.NameLess();
    myClass.DomManipulation();

}

