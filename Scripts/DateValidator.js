const TypedeLocation=document.querySelector(".typeDeLocation");
const dateDebut=document.querySelector(".dateDebut");
const dateFin=document.querySelector(".dateFin");


TypedeLocation.addEventListener("change",(e)=>{
    if (TypedeLocation.value==="Longue"){
     dateFin.value=null;
dateFin.disabled=true;
}
else{
    
    
dateFin.removeAttribute("disabled");    


}   
})
