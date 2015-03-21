import json
import os
from pprint import pprint

scriptPath = os.path.dirname(os.path.realpath(__file__)) + "/"
allGeneratedPostalCodeClasses = {}

def getPostalCodeFormatTemplate(fmt):
    offset = "\t\t\t"
    r = offset + "new PostalCodeFormat {\n"
    r = r + offset + "\tName = \"" + fmt["Name"] + "\",\n"
    r = r + offset + "\tRegexDefault = new Regex(\"" + fmt["RegexDefault"] + "\", RegexOptions.Compiled),\n"
    if "RegexShort" in fmt:
        r = r + offset + "\tRegexShort = new Regex(\"" + fmt["RegexShort"] + "\", RegexOptions.Compiled),\n"
    if "OutputDefault" in fmt:
        r = r + offset + "\tOutputDefault = \"" + fmt["OutputDefault"] + "\",\n"
    if "OutputShort" in fmt:
        r = r + offset + "\tOutputShort = \"" + fmt["OutputShort"] + "\",\n"
    if "AutoConvertToShort" in fmt:
        r = r + offset + "\tAutoConvertToShort = " + fmt["AutoConvertToShort"] + ",\n"
    if "ShortExpansionAsLowestInRange" in fmt:
        r = r + offset + "\tShortExpansionAsLowestInRange = \"" + fmt["ShortExpansionAsLowestInRange"] + "\",\n"
    if "ShortExpansionAsHighestInRange" in fmt:
        r = r + offset + "\tShortExpansionAsHighestInRange = \"" + fmt["ShortExpansionAsHighestInRange"] + "\",\n"
    if "LeftPaddingCharacter" in fmt:
        r = r + offset + "\tLeftPaddingCharacter = \"" + fmt["LeftPaddingCharacter"] + "\",\n"
    if "IgnoreLeftSubstring" in fmt:
        r = r + offset + "\tIgnoreLeftSubstring = \"" + fmt["IgnoreLeftSubstring"] + "\",\n"
    r = r + offset + "}";
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

    offset = "\t\t"

    if "Predecessor" in testData:
        testCases = ""
        for code,prevCode in testData["Predecessor"].iteritems():
            testCases = offset + "[TestCase(\"" + code + "\",\"" + prevCode + "\")]\r\n"
        template = template.replace("@@testsPredecessor@@", testCases[:-2])

    if "Successor" in testData:
        testCases = ""
        for code,nextCode in testData["Successor"].iteritems():
            testCases = offset + "[TestCase(\"" + code + "\",\"" + nextCode + "\")]\r\n"
        template = template.replace("@@testsSuccessor@@", testCases[:-2])

    if "Min" in testData:
        testCases = ""
        for code in testData["Min"]:
            testCases = offset + "[TestCase(\"" + code + "\")]\r\n"
        template = template.replace("@@testsMin@@", testCases[:-2])

    if "Max" in testData:
        testCases = ""
        for code in testData["Max"]:
            testCases = offset + "[TestCase(\"" + code + "\")]\r\n"
        template = template.replace("@@testsMax@@", testCases[:-2])

    if "Valid" in testData:
        testCases = ""
        for code in testData["Valid"]:
            testCases = offset + "[TestCase(\"" + code + "\")]\r\n"
        template = template.replace("@@testsValid@@", testCases[:-2])

    if "Invalid" in testData:
        testCases = ""
        for code in testData["Invalid"]:
            testCases = offset + "[TestCase(\"" + code + "\")]\r\n"
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
        formats = formats + ",\n" + getPostalCodeFormatTemplate(data["Formats"][i])


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
offset = "\t\t\t"
for countryCode, className in allGeneratedPostalCodeClasses.iteritems():
    cases = cases + offset + "\tcase \"" + countryCode + "\":\r\n"
    cases = cases + offset + "\t\treturn new " + className + "(postalCode);\r\n"
generatePostalCodeFactory(cases)

print ""
print "!!! Please make sure the generated files are added to the csproj file."
print ""