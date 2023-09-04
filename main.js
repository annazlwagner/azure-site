//when does this getVisitCount actually get called?
window.addEventListener('DOMContentLoaded', (event) =>{
    getVisitCount();
}) //when the content is loaded, go & run this event - getVisitCount

// declare a constant:
//within the '', we put the URL to the API. We'll have tht 
//later when we implement the API via an Azure function
const functionApi = '';

//this code will grab the JSON that is provided from that API -
//grabbing the correct part of tht JSON & showing it in our HTML

const getVisitCount = () => {
    let count = 30;
    //^declare a variable that holds count data. Can be whatever # bc it will be changed below

    
    fetch(functionApi).then(response => { //fetch the data from that URL that we're providing in functionApi 
        return response.json() //then grab the response & return the response.JSON
    }).then(response =>{ //grab the response
        console.log("Website called function API :D"); //log a message to the console for debugging purposes
        count = response.count; //actually setting tht variable to the data tht's in the json response
        document.getElementById("counter").innerText = count;//grabbing the element in our HTML w/the Id 'counter'
    }).catch(function(error){
        console.log(error);
    });
    //^if there's an error, grab the error & log it to the console
    return count;

}