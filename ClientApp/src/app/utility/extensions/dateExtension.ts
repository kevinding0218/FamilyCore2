interface Date {
    toUTCDateTimeDigits: () => string;
    getWeekStartDate: () => Date;
    getWeekEndDate: () => Date;
}

Date.prototype.toUTCDateTimeDigits = function (): string {
    return this.getUTCFullYear() +
        pad(this.getUTCMonth() + 1) +
        pad(this.getUTCDate()) +
        'T' +
        pad(this.getUTCHours()) +
        pad(this.getUTCMinutes()) +
        'Z';
}

function pad(number) {
    if (number < 10) {
        return '0' + number;
    }
    return number;
}

Date.prototype.getWeekStartDate = function()
{
    //Calcing the starting point
    let start = 0;
    var currentDate = new Date(this.setHours(0, 0, 0, 0));
    var day = currentDate.getDay() - start;
    var date = currentDate.getDate() - day;

        // Grabbing Start/End Dates
    var StartDate = new Date(currentDate.setDate(date));
    return StartDate;
}

Date.prototype.getWeekEndDate = function()
{
    //Calcing the starting point
    let start = 0;
    var currentDate = new Date(this.setHours(0, 0, 0, 0));
    var day = currentDate.getDay() - start;
    var date = currentDate.getDate() - day;

    // Grabbing Start/End Dates
    var EndDate = new Date(currentDate.setDate(date + 6));
    return EndDate;
}
