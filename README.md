# Assignments for CIS310
* Database Management System I
* CIS 310 Wednesday 6:00 - 8:15
* Fall 2016 Semester

Includes all assignments that were given out over the course of the class.

## Grades for assignments
Grades for assignments included in this repository are (Does not include final and participation grade):

| Assignment    | Grade Received       |
| ------------- |:--------------------:|
| 1             | 10/10                |
| 2             | 7/10                 |
| 3             | 11/10                |
| 4             | 9/10                 |
| 5             | 15/15                |
| 6             | 13.3/20              |
| Total         | 65.3/75              |

## Why that grade was given
#### Assignment 1
###### 10/10
>Nice job.
>
>The idea of displaying the error message in the status strip is novel, but you ought to attract user's attention down there somehow.  (Ask me in class.)

After asking about this one, he had mentioned that I should have changed the background or the text color of the status strip to attract attention.

#### Assignment 2
###### 7/10
>Remember, please: when you display an error like (Amount needs to be numeric", be sure to include all information user needs to get it right the next time, such as "Amount needs to be a number from 1,000 to 200,000."
>
>In VB there's no need to do all those type conversions you're used to in C#.
>
>It's neat to use your TestValue routine to validate numbers, but then you had to write custom code for each error message anyway!  Couldn't you devise some generalized way to display the message you needed in TestValue?  such as messagebox.show(value & " needs to be from " & startrange & " to " & endrange)
>
>Where you wrote ElseIf InStr(value, "%") > 0 Then  value = value.ToString().Replace("%", "") / 100 ... you could write simply value = value.replace("%", "")
>
>Remember too that you need to accept both 6 or 0.06 as 6%.
>
>You realize, don't you, that Throw New Exception will crash your program if you're not CATCHING such exceptions?!
>
>Finally, if you're going to put the error message down in the status strip, you need to highlight it in some way -- flash it in red, maybe -- else most users won't notice it.

Coming from ```Swift```, ```Objective-C```, and ```C#``` I was really used to type conversions. Ended up forgetting about them mostly. I threw the errors on purpose. I wanted whoever that was going to use my class improperly to experience the crash. I did not think that I should be the one displaying the dialog box to a user that just says *error.*

Also, did not end up making the correction from Assignment 1 so he mentioned it again.

#### Assignment 3
###### 11/10
> A very nice job!
>
> You don't need to configure the forms in Form Load, if you simply "PerformClick" on each of the menu items.

To the entire class, he mentioned how well I did on this assignment. Did not go into much detail about why it got the grade it did.

#### Assignment 4
###### 9/10
> Very nice look, but no way to reorder stock.

I'm not really sure what he means by no way to reorder stock? I was worried about this one being graded poorly, so I am happy with this grade.

#### Assignment 5
###### 15/15
> Very Nice!

No comments on this one. Felt I did well on the assignment.

#### Assignment 6
###### 13.3/20
>-1 When I add a new Customer, he has no orders, but there's a Detail row in the grid. ???
>
>-1 After I "Save" a Customer, the Undo button remains lit.
>
>-1 ... and you don't reload the tables, so new guy is at the bottom
>
>-1 I can't seem to SAVE an added order.
>
>-1 I can't reassign an order to another customer.

**First one**: That was a bug that had happened last minute that was I was not sure where to begin debugging. **Second**: This must have been a misunderstanding of the requirements. I thought we were supposed to allow an undo after a save, at least that is what made sense to me. **Third**: This also was not a requirement, did not know about this until now. **Fourth**: I had forgotten to add this functionality to that table. **Fifth**: Error occurs here. Must have come up when I was doing my last minute changes for the Order Details table that messed a lot of previous things up.
