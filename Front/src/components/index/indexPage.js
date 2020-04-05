import React from "react";
import SearchBar from './searchBar';
import NotesList from './notesList';
import AddNoteForm from './addNoteForm';

class Index extends React.Component {
    constructor(props) {
        super(props);

        this.getNotes = this.getNotes.bind(this);
        this.deleteNote = this.deleteNote.bind(this);

        this.state = { notes: [] };
        this.getNotes();
    }

    getNotes(search = '') {
        return fetch("api/notes" + (search === '' ? '' : ("?search=" + search)), {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json;charset=utf-8',
                'Accept': 'application/json',
                "Authorization": "Bearer " + this.props.token
            }
        }).then(res => {
            if (res.ok)
                return res.json();
            else
                throw new Error("Error");
        }).then(res => {
            this.setState({ notes: res });
        })
            .catch(err => {
                this.setState({ notes: [] });
            });
    }

    deleteNote(id) {
        if (this.state.notes.some(note => note.id === id))
            return fetch('api/note/delete', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json;charset=utf-8',
                    'Accept': 'application/json',
                    "Authorization": "Bearer " + this.props.token
                },
                body: JSON.stringify({
                    'Id': id
                })
            }).then(res => {
                if (res.ok)
                    this.getNotes();
            });
    }

    render() {
        return (
            <div className="container-notes">
                <SearchBar getNotes={this.getNotes} />
                <NotesList notes={this.state.notes} deleteNote={this.deleteNote} />
                <AddNoteForm getNotes={this.getNotes} token={this.props.token} />
            </div>
        );
    }
}

export default Index;