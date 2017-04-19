# OS-scheduler
this scheduler handles ideal time in FCFS,non pre. SJF and non pre. Priority
in other case handles ideal time only before the first process

If two process arrive at the same time in FCFS the last one inputed will be first 
i don't know if this is right or not but i can simply elemnate this by reversing the list before sorting it
i really hope this is not going to cost me marks

in case of preemptive each "| pi |" equals one time unit
in case of RR each "| pi |" equals (quantam*time unit)
in other cases it equals the full burst time of this process

this scheduler supports only intger inputs

clear button remove all processes
Add button add new processes
in case any missing inputs the scheduler will notifies you
any value in quantam textbox will only affect the output if RR is chosen
if preempitve is chosen it won't affect RR of FCFS outputs
