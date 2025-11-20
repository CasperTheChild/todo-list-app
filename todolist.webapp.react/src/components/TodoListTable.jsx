import PageButton from './PageButton.jsx'
import EditTodoListForm from './EditTodoListForm.jsx'
import { useState } from 'react'

export default function TodoListTable({ todoList, pageNum, setPageNum, handleDelete, handleEdit }) {
    const [editTodoListId, setEditTodoListId] = useState(null);
    const [editTodoListData, setEditTodoListData] = useState({
        title: "",
        description: "",
        startDate: "",
    }
    );

    function openEditTodoListId(id) {
        const itemToEdit = todoList.items.find(t => t.id === id);

        if (itemToEdit) {
            setEditTodoListId(id);
            setEditTodoListData(itemToEdit);
        }
    }

    function closeEditTodoListForm() {
        setEditTodoListId(null);
    }

    console.log("TABLE PROPS:", todoList);

    return (
        <section>
            <h1>Todo Lists</h1>
            <table>
                <thead>
                </thead>
                <tbody>
                    {todoList.items.length > 0 &&
                        todoList.items.map(todo => (
                            <tr key={todo.id}>
                                <td>{todo.title}</td>
                                <td>{todo.description}</td>
                                <td>{todo.startDate}</td>
                                <td>{todo.hasOverdueTask}</td>
                                <td><button onClick={() => handleDelete(todo.id)}>Delete</button></td>
                                <td><button onClick={() => openEditTodoListId(todo.id)}>Edit</button></td>
                            </tr>
                        ))
                    }
                </tbody>
            </table>

            {todoList.totalPages !== pageNum && (
                <PageButton
                    setValue={setPageNum}
                    val={pageNum + 1}
                />
            )}

            {pageNum != 1 && (
                <PageButton
                    setValue={setPageNum}
                    val={pageNum - 1}
                />
            )}

            {editTodoListId !== null && (
                <div>
                    <EditTodoListForm
                        id={editTodoListId}
                        todo={editTodoListData}
                        onEdit={handleEdit}
                        onClose={closeEditTodoListForm}
                    />
                </div>
            )}
        </section>
    )
}