import json
import os
from pprint import pprint

scriptPath = os.path.dirname(os.path.realpath(__file__)) + "/"
allGeneratedPostalCodeClasses = {}

def off(spaces):
    return " " * 4 * spaces

def endl():
    return "\r\n"

def getPostalCodeFormatTemplate(fmt):
    r = off(3) + "new PostalCodeFormat {" + endl()
    r = r + off(4) + "Name = \"" + fmt["Name"] + "\"," + endl()
    r = r + off(4) + "RegexDefault = new Regex(\"" + fmt["RegexDefault"] + "\", RegexOptions.Compiled)," + endl()
    if "RegexShort" in fmt:
        r = r + off(4) + "RegexShort = new Regex(\"" + fmt["RegexShort"] + "\", RegexOptions.Compiled)," + endl()
    if "OutputDefault" in fmt:
        r = r + off(4) + "OutputDefault = \"" + fmt["OutputDefault"] + "\"," + endl()
    if "OutputShort" in fmt:
        r = r + off(4) + "OutputShort = \"" + fmt["OutputShort"] + "\"," + endl()
    if "AutoConvertToShort" in fmt:
        r = r + off(4) + "AutoConvertToShort = " + fmt["AutoConvertToShort"] + "," + endl()
    if "ShortExpansionAsLowestInRange" in fmt:
        r = r + off(4) + "ShortExpansionAsLowestInRange = \"" + fmt["ShortExpansionAsLowestInRange"] + "\"," + endl()
    if "ShortExpansionAsHighestInRange" in fmt:
        r = r + off(4) + "ShortExpansionAsHighestInRange = \"" + fmt["ShortExpansionAsHighestInRange"] + "\"," + endl()
    if "LeftPaddingCharacter" in fmt:
        r = r + off(4) + "LeftPaddingCharacter = \"" + fmt["LeftPaddingCharacter"] + "\"," + endl()
    r = r + off(3) + "}";
    return r;

def loadJsonData(fileName):
    
    with open(scriptPath + fileName) as data_file:
        data = json.load(data_file)

    return data

def generateCountryPostalCode(countryCode):
 
    with open(scriptPath + countryCode + '.json') as data_file:
        data = json.load(data_file)

    generateCountryPostalCodeWithData(countryCode, loadJsonData(countryCode + ".json"))

def generateUnitTestFile(countryCode, testData):

    with open(scriptPath + 'postalcodeunittests.cs.template') as template_file:
        template = "".join(template_file.readlines()) 

    template = template.replace("@@CountryCode@@", countryCode)

    if "Predecessor" in testData:
        testCases = ""
        for code,prevCode in testData["Predecessor"].iteritems():
            testCases = off(2) + "[TestCase(\"" + code + "\",\"" + prevCode + "\")]" + endl()
        template = template.replace("@@testsPredecessor@@", testCases[:-2])

    if "Successor" in testData:
        testCases = ""
        for code,nextCode in testData["Successor"].iteritems():
            testCases = off(2) + "[TestCase(\"" + code + "\",\"" + nextCode + "\")]" + endl()
        template = template.replace("@@testsSuccessor@@", testCases[:-2])

    if "Min" in testData:
        testCases = ""
        for code in testData["Min"]:
            testCases = off(2) + "[TestCase(\"" + code + "\")]" + endl()
        template = template.replace("@@testsMin@@", testCases[:-2])

    if "Max" in testData:
        testCases = ""
        for code in testData["Max"]:
            testCases = off(2) + "[TestCase(\"" + code + "\")]" + endl()
        template = template.replace("@@testsMax@@", testCases[:-2])

    if "Valid" in testData:
        testCases = ""
        for code in testData["Valid"]:
            testCases = off(2) + "[TestCase(\"" + code + "\")]" + endl()
        template = template.replace("@@testsValid@@", testCases[:-2])

    if "Invalid" in testData:
        testCases = ""
        for code in testData["Invalid"]:
            testCases = off(2) + "[TestCase(\"" + code + "\")]" + endl()
        template = template.replace("@@testsInvalid@@", testCases[:-2])

    path = scriptPath + "../src/PostalCodes.UnitTests/Generated/" + countryCode + "PostalCodeTests.gen.cs"
    with open( path, "w") as csgen:
        csgen.write(template) 
        print "Saved Tests to: " + path 

def generateCountryPostalCodeWithData(countryCode, data):

    with open(scriptPath + 'postalcode.cs.template') as template_file:
        template = "".join(template_file.readlines()) 

    formats = getPostalCodeFormatTemplate(data["Formats"][0])
    for i in range(1, len(data["Formats"])):
        formats = formats + "," + endl() + getPostalCodeFormatTemplate(data["Formats"][i])


    template = template.replace("@@CountryCode@@", data["CountryCodeAlpha2"])
    template = template.replace("@@CountryName@@", data["CountryName"])
    template = template.replace("@@WhiteSpaceCharacters@@", data["WhiteSpaceCharacters"])
    template = template.replace("@@Formats@@", formats)

    path = scriptPath + "../src/PostalCodes/Generated/" + countryCode + "PostalCode.gen.cs"
    with open( path, "w") as csgen:
        csgen.write(template) 
        allGeneratedPostalCodeClasses[countryCode] = countryCode + "PostalCode"
        print "Saved to: " + path 

    if "TestData" in data:
        generateUnitTestFile(countryCode, data["TestData"])

def generatePostalCodeFactory(cases):

    with open('postalcodefactory.cs.template') as template_file:
        template = "".join(template_file.readlines())         

    template = template.replace("@@cases@@", cases)

    path = scriptPath + "../src/PostalCodes/Generated/PostalCodeFactory.gen.cs"
    with open( path, "w") as csgen:
        csgen.write(template) 
        allGeneratedPostalCodeClasses[countryCode] = countryCode + "PostalCode"
        print "Saved to: " + path 

# Global 4-digit
filesForCountries = {
        "4Digits.json" : ["AT", "AU", "BG", "CH", "DK", "HU", "NL", "NO", "SI", "NZ", "BE", "CY"],
        "5Digits.json" : ["DE","CZ","EE","ES","FI","FR","GR","IT","PL","SE","SK","TR","US","PR","VI","AS","GU","MP","PW","FM","MH","MY","HR","MX"],
        "6Digits.json" : ["IN", "SG"],
        "7Digits.json" : ["JP"]
        }

# Generate classes for the numeric format countries
for jsonFile,countryCodeList in filesForCountries.iteritems():
    for countryCode in countryCodeList:
        data = loadJsonData(jsonFile)
        data["CountryName"] = countryCode
        data["CountryCodeAlpha2"] = countryCode
        generateCountryPostalCodeWithData(countryCode, data)

# Generate classes for special countries
countries = []
listing = os.listdir(scriptPath)
for files in listing:
    if files.endswith('.json'):
        code = files.split('.')[0];
        if len(code) != 2: 
            continue
        countries = countries + [code]

for countryCode in countries:
    generateCountryPostalCode(countryCode)

cases = "";
for countryCode, className in allGeneratedPostalCodeClasses.iteritems():
    cases = cases + off(4) + "case \"" + countryCode + "\":" + endl()
    cases = cases + off(5) + "return new " + className + "(postalCode);" + endl()
generatePostalCodeFactory(cases)

print ""
print "All generated *.gen.cs files are added in the csproj file with wildcard."
print ""