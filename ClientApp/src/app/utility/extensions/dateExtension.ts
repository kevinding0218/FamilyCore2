interface Date {
    toUTCDateTimeDigits: () => string;
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