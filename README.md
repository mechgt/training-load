# Training Load

Training Load plugin for SportTracks (Desktop).  Track your fitness, target race day, and avoid overtraining through HR and power based analysis.

Use this plugin to track your fitness, target race day, and avoid overtraining by charting your CTL (Chronic Training Load, a 'fitness' indicator), ATL (Acute Training Load, 'fatigue' indicator), and TSB (Training Stress Balance, 'freshness' indicator).  These charts indicate a moving average of your training intensity by assigning a score to each activity, and can be broken down by activity type (running, cycling, etc.) - perfect for triathletes.

This software was open-sourced after SportTracks desktop was discontinued, and all restrictions have been removed.

Compiled plugin files are available under [Releases](https://github.com/mechgt/training-load/releases).

![tl_main](https://mechgt.com/st/images/tl_main.png)

### Getting Started
Training Load adds a new view to SportTracks.  Access Training Load as shown below:

![tl_menu](https://mechgt.com/st/images/tl_menu.png)

Assuming you have activities with HR data tracks and or power tracks, this is the absolute bare minimum to get you started.  However, there are a few setup steps you'll want to do in order to get more accurate and usable results.

### Multi-Sport
From the main display, check out the menu options shown below. This will change your chart between power (Training Stress Score) and HR based (Trimp) analysis, and can itemize scores from different sports.

![tl_menu](https://mechgt.com/st/images/tl_multisport_chart.png)

### Power Based Training Setup Steps
**Background**: Power based activity scores (TSS or Training Stress Score) are relative to your ability at the time of the activity.  This reference point is known as your FTP (Functional Threshold Power), or "1 hour power".  This is the best average power you can maintain for 1 hour; in a Time Trial race for instance.

**Setup**: To setup FTP, go to the custom fields page in Athlete view and find *FTPcycle* and *FTPrun*:

![tl_ftpentry](https://mechgt.com/st/images/tl_ftpentry.png)

FTP values will be needed beginning with the date your logbook starts.  FTP may rarely change, so it's possible that this single entry is your only entry, or if you've already been tracking this value in your training then you may enter 1 or 2 (or more) FTP test results per year.

As the name suggests, FTPcycle is for cycling activities, and FTPrun is for running activities.  PowerRunner plugin (https://github.com/mechgt/power-runner) is required to differentiate running from cycling activities.

NOTE: If *FTPcycle*/*FTPrun* is not entered or is disabled on the settings page, 250 watts is assumed as the default for all TSS calculations.

### Heart Rate Based Training Setup Steps
**Background**: Heart rate based activity scores (TRIMP or TRaining IMPulse) are derived from total time spent in various heart rate zones.  Short duration, high intensity training may result in a score similar to a much longer duration, but low intensity activity.

**Setup**: (This procedure assumes default/automatic settings, which should be appropriate for most people)

1) Set your max & rest HR in athlete settings. Set it far back in history... as early as the first activity in your logbook. Feel free to adjust it as time goes on (shouldn't move too much or too often realistically). Even if you don't know take a guess at what your old numbers should be.  Make sure you've got a value entered for the earliest activity date in your logbook.  HRrest and HRmax values will be used to calculate TRIMP for these activities, so they'll all need a value associated with them.  1 entry per year might be typical.  
![tl_menu](https://mechgt.com/st/images/tl_athletehist.png)
2) Ensure **Automatic Mode** is checked on the Training Load Settings page:
![tl_menu](https://mechgt.com/st/images/tl_heartzonesettings.png)

NOTE: Automatic Mode uses Max HR and Resting HR (set previously) to calculate TRIMP.  As time passes and your HR profile changes, this allows your TRIMP calculation profile to dynamically change as well (as opposed to a static unchanging profile).

**Manual/Advanced HR factor configuration (optional)**  
Uncheck **Automatic Mode**.  Create a new heart rate category with approximately 10 heart rate zones evenly spread between a low HR and your max hr.  Each zone might cover be 6 - 8 bpm, and your lowest zone would be relatively inactive.  We're using this simply to do "poor man's calculus", so you don't need to use your Joe Friel HR training zones or any other highly specialized zones like that.

![tl_menu](https://mechgt.com/st/images/tl_addcategory.png)

Back in the Training Load settings page, check Single Zone and select your newly created HR zone from the dropdown list (named TRIMP in the example above).

Be sure to check out the settings page to setup your factors and customize how your charts are calculated.  The plugin will initially guess the factors to associate with each HR zone.  You may also want to setup another custom HR zone dedicated specifically to calculating TRIMP.


