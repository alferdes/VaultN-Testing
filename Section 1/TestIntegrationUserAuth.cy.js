describe('User Authentication Flow', () => {
  it('should register a new user and log them in', () => {
    cy.visit('/register');
    cy.get('#username').type('newuser');
    cy.get('#email').type('newuser@example.com');
    cy.get('#password').type('securepass123');
    cy.get('#register-button').click();

    cy.url().should('include', '/dashboard');
    cy.get('#welcome-message').should('contain', 'Welcome, newuser');

    // Test API for new user data
    cy.request('GET', '/api/user/profile')
      .its('status')
      .should('equal', 200);
    cy.request('GET', '/api/user/profile')
      .its('username')
      .should('equal', 'newuser');
  });

  it('should login an existing user, navigate to dashboard and then logout', () => {
    cy.visit('/login');
    cy.get('#username').type('testuser1');
    cy.get('#password').type('testpass123');
    cy.get('#login-button').click();

    cy.url().should('include', '/dashboard');
    cy.get('#welcome-message').should('contain', 'Welcome, testuser1');

    cy.get('#logout-button').click();

    cy.url().should('include', '/login');
    cy.get('#logout-message').should('contain', 'Logout successful');
  });

  it('should throw an error for wrong username', () => {
    cy.visit('/login');
    cy.get('#username').type('wrongname1');
    cy.get('#password').type('testpass123');
    cy.get('#login-button').click();

    cy.url().should('include', '/login');
    cy.get('#error-message').should('contain', 'Wrong username or password');
  });

  it('should throw an error for wrong password', () => {
    cy.visit('/login');
    cy.get('#username').type('testuser1');
    cy.get('#password').type('wrongpass');
    cy.get('#login-button').click();

    cy.url().should('include', '/login');
    cy.get('#error-message').should('contain', 'Wrong username or password');
  });

});