# Assignments for CIS310
Database Management System I - CIS 310 Wednesday 6:00 - 8:15

## Grades for assignments
Grades for assignments included in this repository are:

| Assignment    | Are                  |
| ------------- |:--------------------:|
| 1             | 10/10                |
| 2             | 7/10                 |
| 3             | 11/10                |
| 4             | *No grade available* |
| 5             | *No grade available* |
| 6             | *No grade available* |

## Why that grade was given
#### Assignment 1
>Nice job.
>
>The idea of displaying the error message in the status strip is novel, but you ought to attract user's attention down there somehow.  (Ask me in class.)

#### Assignment 2
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

#### Assignment 3
> A very nice job!
>
> You don't need to configure the forms in Form Load, if you simply "PerformClick" on each of the menu items.
