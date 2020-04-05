import React from "react";
import AuthorizationForm from './authorization/authorizationForm';
import Index from './index/indexPage';

class Body extends React.Component {
    constructor(props) {
        super(props);

        this.redirect = this.redirect.bind(this);
        this.historyBack = this.historyBack.bind(this);
        this.authorize = this.authorize.bind(this);
        this.unAuthorizeRedirect = this.unAuthorizeRedirect.bind(this);
        this.logout = this.logout.bind(this);

        this.state = { page: this.props.page, isAuthorize: false, authorizeChecked: false, token: '' };

        window.onpopstate = event => {
            this.historyBack(event.target.location.pathname);
        };
        this.authorize(localStorage.getItem('token'));
    }

    redirect(page) {
        if (!this.state.isAuthorize)
            if (page !== this.props.pages.index) {
                if (this.state.page != page) {
                    history.pushState(null, null, page);
                    this.setState({ page: page });
                }
            }
            else {
                if (this.state.page != this.props.pages.login) {
                    history.pushState(null, null, this.props.pages.login);
                    this.setState({ page: this.props.pages.login });
                }
            }
        else {
            if (page != this.props.pages.index && this.state.page != this.props.pages.index) {
                history.pushState(null, null, this.props.pages.index);
                this.setState({ page: this.props.pages.index });
            }
        }
    }

    historyBack(page) {
        if (!this.state.isAuthorize)
            if (page !== this.props.pages.index) {
                if (this.state.page != page) {
                    this.setState({ page: page });
                }
            }
            else {
                if (this.state.page != this.props.pages.login) {
                    history.pushState(null, null, this.props.pages.login);
                    this.setState({ page: this.props.pages.login });
                }
                else
                    history.pushState(null, null, this.props.pages.login);
            }
        else {
            if (page != this.props.pages.index) {
                if (this.state.page != this.props.pages.index) {
                    history.pushState(null, null, this.props.pages.index);
                    this.setState({ page: this.props.pages.index });
                }
                else {
                    history.pushState(null, null, this.props.pages.index);
                }
            }
        }
    }

    authorize(token = '') {
        if (token === null) {
            token = '';
        }
        return fetch('api/authorize', {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                "Authorization": "Bearer " + token
            }
        }).then(res => {
            if (res.ok) {
                localStorage.setItem('token', token);
                if (this.state.page != this.props.pages.index)
                    history.pushState(null, null, this.props.pages.index);
                this.setState({ isAuthorize: true, authorizeChecked: true, page: this.props.pages.index, token: token });
            }
            else if (res.status === 401) {
                this.unAuthorizeRedirect();
            }
        });
    }

    unAuthorizeRedirect() {
        let unAuthorizedPage = this.state.page === this.props.pages.register || this.state.page === this.props.pages.login ? this.state.page : this.props.pages.login;

        if (this.state.page != unAuthorizedPage)
            history.pushState(null, null, unAuthorizedPage);
        this.setState({
            isAuthorize: false,
            authorizeChecked: true,
            token: '',
            page: unAuthorizedPage
        })
    }

    logout(event) {
        event.preventDefault();
        localStorage.removeItem('token');
        history.pushState(null, null, this.props.pages.login);
        this.setState({ isAuthorize: false, token: '', page: this.props.pages.login });
    }

    render() {
        if (this.state.authorizeChecked) {
            if (this.state.page === this.props.pages.register || this.state.page === this.props.pages.login)
                return <AuthorizationForm authorize={this.authorize} redirect={this.redirect} page={this.state.page} pages={this.props.pages} />;
            else
                return (
                    <div className="container-center">
                        <Index token={this.state.token} />
                        <button className="btn btn-primary btn-logout" onClick={this.logout}>Log out</button>
                    </div>
                );
        }
        else {
            return <div></div>
        }
    }
}

export default Body;