Feature: Library Management

Scenario: Create a new library
    Given I have a library resource with title "Sample Title" and description "Sample Description"
    When I send a POST request to "/api/v1/library" with the library resource
    Then the response status code should be 201
    And the response should contain the created library with title "Sample Title"
