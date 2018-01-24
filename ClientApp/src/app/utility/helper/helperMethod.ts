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

    static subscribeErrorHandler(err, toastr) {
        if (err.status === 400) {
            // handle validation error
            let validationErrorDictionary = JSON.parse(err.text());
            for (var fieldName in validationErrorDictionary) {
                if (validationErrorDictionary.hasOwnProperty(fieldName)) {
                    toastr.warning(validationErrorDictionary[fieldName], 'Invalid Operation');
                }
            }
        }
    }
}
