Feature: Multimedia Management

  Scenario: Creating a multimedia resource with an invalid URL
    Given I have a multimedia resource with an invalid URL
    When I request to create the multimedia resource
    Then the request should be rejected

  Scenario: Creating a multimedia resource with a valid URL
    Given I have a multimedia resource with a valid URL
    When I request to create the multimedia resource
    Then the multimedia resource should be created successfully
