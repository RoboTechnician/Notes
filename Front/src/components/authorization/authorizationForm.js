import React from "react";

class AuthorizationForm extends React.Component {
    constructor(props) {
        super(props);

        this.submit = this.submit.bind(this);
        this.emailChange = this.emailChange.bind(this);
        this.passwordChange = this.passwordChange.bind(this);
        this.changeAuthorizationPage = this.changeAuthorizationPage.bind(this);

        this.state = { email: '', password: '', emailError: '', passwordError: '', page: this.props.page };
    }

    submit(event) {
        event.preventDefault();

        let url = this.props.page === this.props.pages.register ? "api/register" : "api/login";
        let error = false;

        return fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify({
                'Email': this.state.email,
                'Password': this.state.password
            })
        }).then(res => {
            if (res.ok)
                return res.json();
            else {
                if (res.status >= 500)
                    throw new Error("Internal server error");
                else {
                    error = true;
                    if (this.props.page === this.props.pages.register)
                        return res.json();
                    else
                        return res.text();
                }
            }
        }).then(res => {
            if (error) {
                if (this.props.page === this.props.pages.register)
                    this.setState({ emailError: res.email, passwordError: res.password });
                else
                    this.setState({ passwordError: res });
            }
            else {
                this.props.authorize(res.token);
            }
        }).catch(err => this.setState({ passwordError: err }));
    }

    emailChange(event) {
        this.setState({ email: event.target.value });
    }

    passwordChange(event) {
        this.setState({ password: event.target.value });
    }

    changeAuthorizationPage(event) {
        event.preventDefault();
        this.props.redirect(this.props.page === this.props.pages.register ? this.props.pages.login : this.props.pages.register);
    }

    render() {
        if (this.props.page != this.state.page) {
            this.state.page = this.props.page;
            this.state.emailError = '';
            this.state.passwordError = '';
        }

        return (
            <form className="form-login" onSubmit={this.submit}>
                <h1 className="h3 mb-3 font-weight-normal">{this.props.page === this.props.pages.register ? 'Please Register' : 'Please Log in'}</h1>
                <input className="form-control" value={this.state.email} type="email" placeholder="Email" onChange={this.emailChange} />
                <label className="error">{this.state.emailError}</label>
                <input className="form-control" value={this.state.password} type="password" placeholder="Password" onChange={this.passwordChange} />
                <label className="error">{this.state.passwordError}</label>
                <button className="btn btn-lg btn-primary btn-block mt-3" type="submit">{this.props.page === this.props.pages.register ? 'Register' : 'Log in'}</button>
                <a href={this.props.page === this.props.pages.register ? this.props.pages.login : this.props.pages.register} onClick={this.changeAuthorizationPage}>{this.props.page === this.props.pages.register ? 'Log in' : 'Register'}</a>
            </form>
        );
    }
}

export default AuthorizationForm;