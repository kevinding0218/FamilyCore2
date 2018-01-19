export class HelperMethod {
    static isDuplicateItemInArray(arrayObj, key) {
        let duplicateArr = [];
        let non_duplicateArr = [];
        arrayObj.forEach(element => {
            if (!non_duplicateArr.includes(element[key]))
                non_duplicateArr.push(element[key]);
            else
                duplicateArr.push(element[key]);
        });

        if (duplicateArr.length > 0) {
            return true;
        } else {
            return false;
        }
    }

    static findAnotherAttributeByKey(arrayObj, providedKey, providedValue, outputKey) {
        var outputValue = 'NotFound';
        arrayObj.forEach(element => {
            //console.log('element[providedKey] is ' + element[providedKey] + 'providedValue is ' + providedValue);
            if (element[providedKey].toString() == providedValue.toString())
                outputValue = element[outputKey];
        });

        return outputValue;
    }
}
