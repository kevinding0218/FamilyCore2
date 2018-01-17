interface String {  
    capitalizeFirstLetter: () => string;
}

String.prototype.capitalizeFirstLetter = function() : string {
    if (this != null && this != '')
        return this.charAt(0).toUpperCase() + this.slice(1);
    else
        return '';
}