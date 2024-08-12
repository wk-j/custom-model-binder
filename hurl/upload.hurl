# Test file upload
POST http://localhost:8080/upload
[MultipartFormData]
file: file,test.txt;
Content-Type: text/plain
description: Test file upload description
category: TestCategory
upload_date: 2023-08-12T10:30:00Z
metadata: {"key1": "value1", "key2": "value2"}

HTTP 200
[Asserts]
jsonpath "$.file_name" == "test.txt"
jsonpath "$.description" == "Test file upload description"
jsonpath "$.category" == "TestCategory"
jsonpath "$.upload_date" == "2023-08-12T10:30:00Z"
jsonpath "$.metadata_count" == 2
